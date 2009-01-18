﻿namespace AjGa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPopulation<G, V>
    {
        List<IGenome<G, V>> Genomes { get; }
    }
}
