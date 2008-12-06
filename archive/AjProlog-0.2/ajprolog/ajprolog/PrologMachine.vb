' +---------------------------------------------------------------------+
' | ajprolog - A Prolog Interpreter                                     |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2004 Angel J. Lopez. All rights reserved.        |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajprolog Software License,       |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://www.ajlopez.net/ajprolog/license.txt.                     |
' +---------------------------------------------------------------------+
'
'
Imports System.IO

Public Class PrologMachine
    Private mFacts As New FactBase()
    Private mVariables As IList = New ArrayList()
    Private mBindings As IList = New ArrayList()
    Private mPendings As IList = New ArrayList()
    Private mNodes As IList = New ArrayList()
    Private mQueryVariables As IList = New ArrayList()
    Private mLevel As Integer
    Private mTrace As Boolean

    Private mInput As TextReader = Console.In
    Private mOutput As TextWriter = Console.Out

    Private mInteractive As Boolean

    Private mNode As Node
    Private mAction As PrologMachineAction

    Public Property Interactive() As Boolean
        Get
            Return mInteractive
        End Get
        Set(ByVal Value As Boolean)
            mInteractive = Value
        End Set
    End Property

    Public Property Trace() As Boolean
        Get
            Return mTrace
        End Get
        Set(ByVal Value As Boolean)
            mTrace = True
        End Set
    End Property

    Public ReadOnly Property Input() As TextReader
        Get
            Return mInput
        End Get
    End Property

    Public ReadOnly Property Output() As TextWriter
        Get
            Return mOutput
        End Get
    End Property

    Public Property Level() As Integer
        Get
            Return mLevel
        End Get
        Set(ByVal Value As Integer)
            mLevel = Value
        End Set
    End Property

    Public Property NVariables() As Integer
        Get
            Return mVariables.Count
        End Get

        Set(ByVal Value As Integer)
            While mVariables.Count > Value
                mVariables.RemoveAt(mVariables.Count - 1)
            End While

            While mVariables.Count < Value
                mVariables.Add(New Variable(mVariables.Count, Me))
            End While
        End Set
    End Property

    Public Property NBindings() As Integer
        Get
            Return mBindings.Count
        End Get
        Set(ByVal Value As Integer)
            Dim v As Variable
            While mBindings.Count > Value
                v = mBindings(mBindings.Count - 1)
                v.Unbind()
                mBindings.RemoveAt(mBindings.Count - 1)
            End While
        End Set
    End Property

    Public Property NPendings() As Integer
        Get
            Return mPendings.Count
        End Get
        Set(ByVal Value As Integer)
            Dim v As Variable
            While mPendings.Count > Value
                mPendings.RemoveAt(mPendings.Count - 1)
            End While
            If mPendings.Count < Value Then
                Throw New Exception("Pocos Pendientes")
            End If
        End Set
    End Property

    Public ReadOnly Property Bindings() As IList
        Get
            Return mBindings
        End Get
    End Property

    Public Function PopNode() As Node
        If mNodes.Count = 0 Then
            Return Nothing
        End If

        Dim node As node

        node = mNodes(mNodes.Count - 1)
        mNodes.RemoveAt(mNodes.Count - 1)

        Return node
    End Function

    Public Sub PushNode(ByVal node As Node)
        If Not node Is Nothing AndAlso node.IsPushable Then
            mNodes.Add(node)
        End If
    End Sub

    Public Sub PushPending(ByVal po As PrologObject)
        If Not po Is Nothing Then
            mPendings.Add(po.MakeNode(Me))
        End If
    End Sub

    Public Sub PushPending(ByVal node As Node)
        If Not node Is Nothing Then
            mPendings.Add(node)
        End If
    End Sub

    Public Function PopPending() As Node
        If mPendings.Count = 0 Then
            Return Nothing
        End If

        Dim n As Node

        n = mPendings(mPendings.Count - 1)
        mPendings.RemoveAt(mPendings.Count - 1)

        Return n
    End Function

    Public Function Assertz(ByVal po As PrologObject)
        mFacts.Add(po)
    End Function

    Public Function GetFacts(ByVal po As PrologObject) As IList
        Return mFacts.GetFacts(po)
    End Function

    Public Function GetPredicates(ByVal atom As StringObject) As IList
        Return mFacts.GetPredicates(atom)
    End Function

    Function DoSolution() As Boolean
        If mQueryVariables.Count = 0 Then
            Console.WriteLine("yes")
            Return True
        End If

        Dim k As Integer

        For k = 0 To mQueryVariables.Count - 1
            Console.WriteLine(mQueryVariables(k).ToString & ": " & Variable(k).Dereference.ToString())
        Next

        While Console.In.Peek > 0
            Console.In.Read()
        End While

        Dim line As String = Console.In.ReadLine()

        If line = ";" Then
            Return False
        End If

        Return True
    End Function

    Sub DoNoSolution()
        Console.WriteLine("no")
    End Sub

    Sub ExecuteTrace()
        Dim n As Integer
        Console.WriteLine("Nivel " & Level)
        Console.WriteLine("Variables")

        For n = 0 To NVariables - 1
            Console.WriteLine("n: " & Variable(n).Dereference.ToString)
        Next

        Console.WriteLine("Pendientes")

        For n = 0 To mPendings.Count - 1
            Console.WriteLine(DirectCast(mPendings(n), Node).Object.ToString)
        Next

        Console.WriteLine("Nodos")

        For n = 0 To mNodes.Count - 1
            Console.WriteLine(DirectCast(mNodes(n), Node).Object.ToString)
        Next
    End Sub

    Public Function Resolve(ByVal po As PrologObject) As Boolean
        mVariables.Clear()
        mBindings.Clear()
        mPendings.Clear()
        mNodes.Clear()
        mQueryVariables.Clear()
        mLevel = 0

        Dim vars As New ArrayList()

        po = AdjustVariables(po, vars, mVariables.Count)

        If Interactive Then
            mQueryVariables = vars
        End If

        PushPending(po)

        Dim node As node
        Dim result As Boolean

        result = True

        While True
            If Trace Then
                ExecuteTrace()
            End If
            If result Then
                node = PopPending()
                If node Is Nothing Then
                    If Interactive Then
                        If DoSolution() Then
                            Return True
                        Else
                            result = False
                        End If
                    Else
                        Return True
                    End If
                Else
                    result = node.ExecuteCall()
                    'If result Then
                    '    PushNode(node)
                    'Else
                    '    PushPending(node.Object)
                    'End If
                End If
            Else
                node = PopNode()
                If node Is Nothing Then
                    If Interactive Then
                        DoNoSolution()
                    End If
                    Return False
                End If
                result = node.ExecuteRedo
                'If result Then
                '    PushNode(node)
                'Else
                '    PushPending(node.Object)
                'End If
            End If
        End While
    End Function

    Public ReadOnly Property Variable(ByVal n As Integer) As Variable
        Get
            If n < 0 Then
                Throw New ArgumentException("Nro. de Variable incorrecto " + n)
            End If

            If n >= mVariables.Count Then
                Dim k As Integer

                For k = mVariables.Count To n
                    mVariables.Add(New Variable(k, Me))
                Next
            End If

            Return mVariables(n)
        End Get
    End Property

    Function AdjustVariables(ByVal po As PrologObject, ByVal vars As ArrayList, ByVal offset As Integer)
        If TypeOf po Is StructureObject Then
            po = DirectCast(po, StructureObject).Normalize
        End If

        If TypeOf po Is StringObject AndAlso DirectCast(po, StringObject).IsVariableName Then
            Dim off As Integer
            off = vars.IndexOf(po)

            If off >= 0 Then
                Return Variable(offset + off)
            End If

            vars.Add(po)

            po = Variable(offset + vars.Count - 1)
        ElseIf TypeOf po Is StructureObject Then
            po = New StructureObject(DirectCast(po, StructureObject), Me, vars, offset)
        End If

        Return po
    End Function

    Public Function AdjustVariables(ByVal po As PrologObject) As PrologObject
        Return AdjustVariables(po, New ArrayList(), mVariables.Count)
    End Function

    Public Function Unify(ByVal po1 As PrologObject, ByVal po2 As PrologObject) As Boolean
        po1 = po1.Dereference
        po2 = po2.Dereference

        If po1.Equals(po2) Then
            Return True
        End If

        If TypeOf po1 Is Variable Then
            DirectCast(po1, Variable).Bind(po2)
            Return True
        End If

        If TypeOf po2 Is Variable Then
            DirectCast(po2, Variable).Bind(po1)
            Return True
        End If

        If TypeOf po1 Is StructureObject AndAlso TypeOf po2 Is StructureObject Then
            Dim st1 As StructureObject = DirectCast(po1, StructureObject)
            Dim st2 As StructureObject = DirectCast(po2, StructureObject)

            If Not Unify(st1.Functor, st2.Functor) Then
                Return False
            End If

            If Not st1.Arity = st2.Arity Then
                Return False
            End If

            Dim k As Integer

            For k = 0 To st1.Arity - 1
                If Not Unify(st1.Parameters(k), st2.Parameters(k)) Then
                    Return False
                End If
            Next

            Return True
        End If

        Return False
    End Function

    Public Sub NextAction(ByVal node As Node, ByVal action As PrologMachineAction)
        mNode = node
        mAction = action
    End Sub

    Public Function GetCutNode() As FactNode
        Dim k As Integer

        For k = mNodes.Count - 1 To 0 Step -1
            If TypeOf mNodes(k) Is FactNode Then
                Return DirectCast(mNodes(k), FactNode)
            End If
        Next

        Return Nothing
    End Function

    Public Sub CutToNode(ByVal node As FactNode)
        Dim n As node

        While mNodes.Count > 0
            n = mNodes(mNodes.Count - 1)
            If n Is node Then
                DirectCast(node, FactNode).DoCut()
                Return
            End If
            mNodes.RemoveAt(mNodes.Count - 1)
        End While
    End Sub

    Public Property Status() As PrologMachineStatus
        Get
            Dim st As New PrologMachineStatus()
            st.NVariables = NVariables
            st.NBindings = NBindings
            st.NPendings = NPendings
            st.Level = Level
            Return st
        End Get
        Set(ByVal Value As PrologMachineStatus)
            NVariables = Value.NVariables
            NBindings = Value.NBindings
            NPendings = Value.NPendings
            Level = Value.Level
        End Set
    End Property
End Class

Public Enum PrologMachineAction
    [Call] = 1
    [Exit] = 2
    Redo = 3
    Fail = 4
End Enum

Public Class PrologMachineStatus
    Public NVariables As Integer
    Public NBindings As Integer
    Public NPendings As Integer
    Public Level As Integer
End Class