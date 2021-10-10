using GoodEnough.Biology.Cells;
using System.Collections.Generic;

namespace GoodEnough.Biology.Systems.Nervous.Cells
{
    public class BaseNeuron : Cell
    {
        // Rough outlining here, mainly just setting up structure.
        public List<Synapse> Inputs { get; init; } = new();
        public List<Synapse> Outputs { get; init; } = new();
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Synaptic_plasticity
    /// </summary>
    public struct Synapse
    {
        /// <summary>
        /// The <seealso cref="Cell"/> that is receiving a signal from the synapse.
        /// </summary>
        public Cell PostSynapticCell { get; init; }

        /// <summary>
        /// The 'stength' of this synapse. This will change later...
        /// https://en.wikipedia.org/wiki/Chemical_synapse#Synaptic_strength
        /// In reality, strength of a synapse is the amount of receptors, proximity, likelyhood of being released, etc.
        /// </summary>
        public float Strength { get; set; }
    }
}
