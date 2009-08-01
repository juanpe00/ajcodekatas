﻿namespace AjBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Table
    {
        private string name;
        private Schema schema;
        private List<Column> columns = new List<Column>();

        internal Table(Schema schema, string name)
        {
            this.schema = schema;
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public Schema Schema { get { return this.schema; } }

        public Database Database { get { return this.schema.Database; } }

        public void AddColumn(Column column)
        {
            this.columns.Add(column);
            column.SetTable(this);
        }

        public Column GetColumn(string name)
        {
            return this.columns.Single(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
