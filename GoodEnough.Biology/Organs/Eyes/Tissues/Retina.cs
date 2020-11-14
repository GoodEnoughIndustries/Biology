using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Organs.Eyes.Tissues
{
    // https://en.wikipedia.org/wiki/Retina
    public class Retina
    {
        public Retina(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentException($"Width of {nameof(Retina)} visual input must be greater than 0.");
            }

            if (height <= 0)
            {
                throw new ArgumentException($"Height of {nameof(Retina)} visual input must be greater than 0.");
            }

            this.Width = width;
            this.Height = height;
            this.Grid = new byte[this.Width, this.Height];

            this.InnerLimitingMembraneLayer = new();
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public byte[,] Grid { get; init; }

        public InnerLimitingMembraneLayer InnerLimitingMembraneLayer { get; init; }
    }

    // https://en.wikipedia.org/wiki/Retina#Retinal_layers
    /*
     * These layers can be grouped into 4 main processing stages:
     * photoreception; transmission to bipolar cells; transmission
     * to ganglion cells, which also contain photoreceptors,
     * the photosensitive ganglion cells; and transmission along
     * the optic nerve. At each synaptic stage there are also laterally
     * connecting horizontal and amacrine cells.
     * */

    // https://en.wikipedia.org/wiki/Inner_limiting_membrane
    public class InnerLimitingMembraneLayer
    {
        // has mullercells

        public NerveFiberLayer NerveFiberLayer { get; init; } = new();
    }

    //https://en.wikipedia.org/wiki/Retinal_nerve_fiber_layer
    public class NerveFiberLayer
    {
        // nerve fibers / axonal pattern connecting layers
        public GanglionCellLayer GanglionCellLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Ganglion_cell_layer
    public class GanglionCellLayer
    {
        // contains retinalganglioncells

        public InnerPlexiformLayer InnerPlexiformLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Inner_plexiform_layer
    public class InnerPlexiformLayer
    {
        // dendrites from ganglion cell layer and inner nuclear layer
        public InnerNuclearLayer InnerNuclearLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Inner_nuclear_layer
    public class InnerNuclearLayer
    {
        // bipolar cells, horizontal cells, and amacrine cells
        // bipolar most numerous - connect to rods/cones

        // horizontal cells tons of dendrites to outerplexiformlayer
        // axons to same layer

        public OuterPlexiformLayer OuterPlexiformLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Outer_plexiform_layer
    public class OuterPlexiformLayer
    {
        // synapse layer dendrites of horizontal cells from innernuclear
        // to photo receptor cell inner segments in outernuclearlayer
        public OuterNuclearLayer OuterNuclearLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Outer_nuclear_layer
    public class OuterNuclearLayer
    {
        // part that detects light
        // rod and cone granules

        public ExternalLimitingMembraneLayer ExternalLimitingMembraneLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/External_limiting_membrane
    public class ExternalLimitingMembraneLayer
    {
        // membrane separating rod/cone receptor's from their nucleuses
        public LayerOfRodsAndConesLayer LayerOfRodsAndConesLayer { get; init; } = new();

    }

    // https://en.wikipedia.org/wiki/Layer_of_rods_and_cones
    public class LayerOfRodsAndConesLayer
    {
        // layer of rod and cone receptors

        public RetinalPigmentEpitheliumLayer RetinalPigmentEpitheliumLayer { get; init; } = new();
    }

    // https://en.wikipedia.org/wiki/Retinal_pigment_epithelium
    public class RetinalPigmentEpitheliumLayer
    {
        // hexagonal cells of pigment
        // tons of stuff for health of eye
    }
}
