using GoodEnough.Biology.Cells.PhotoReceptors;
using GoodEnough.Biology.Organs.Eyes.Cells;
using GoodEnough.Biology.Structural;
using GoodEnough.Biology.Systems.Nervous.Cells.Glials;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Runtime.CompilerServices;

namespace GoodEnough.Biology.Organs.Eyes.Tissues.Retinal
{
    // https://en.wikipedia.org/wiki/Retina
    /// <summary>
    /// Represents a human retina.
    /// The layers of a real retina are represented, but most are empty here.
    /// This is because a lot of the structure is for structural support and positioning,
    /// that we don't have to worry about.
    /// </summary>
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
            this.PhotoReceptors = new(this.Width * 4, this.Height * 4);

            this.PopulatePhotoReceptors(this.PhotoReceptors);

            this.InnerLimitingMembraneLayer = new(new RetinalLayerContext(this.Width, this.Height, this));

            // following scope writes a png of the photoreceptors color-coded...
            // move behind debug flag later.
            {
                var img = new Image<Rgba32>(this.PhotoReceptors.Width, this.PhotoReceptors.Height);

                for (var x = 0; x < this.PhotoReceptors.Width; x++)
                {
                    for (var y = 0; y < this.PhotoReceptors.Height; y++)
                    {
                        img[x, y] = this.PhotoReceptors[x, y] switch
                        {
                            RodCell => Color.Black,
                            LConeCell => Color.Red,
                            MConeCell => Color.Green,
                            SConeCell => Color.Blue,
                            _ => Color.Transparent,
                        };
                    }
                }

                img.SaveAsPng(@"C:\Projects\GoodEnough.Biology\map.png");
            }
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public Layer<IPhotoReceptorCell> PhotoReceptors { get; init; }

        public InnerLimitingMembraneLayer InnerLimitingMembraneLayer { get; init; }

        private void PopulatePhotoReceptors(Layer<IPhotoReceptorCell> layer)
        {
            // TODO: allow photoreceptor layer to be passed in,
            // as different creatures have different ratios, or even photoreceptors...
            // right now, use human numbers.

            // 120m rods to 6m cones = 20:1 r:c
            // 3m ganglions with 2% being photosensitive

            // set everything to Rods, they're by far the majority...
            // overwrite randomly with other ones
            for (var x = 0; x < layer.Width; x++)
            {
                for (var y = 0; y < layer.Height; y++)
                {
                    layer[x, y] = new RodCell();
                }
            }

            var numberOfCones = layer.Count / 20;
            var r = new Random();

            for (var c = 0; c < numberOfCones; c++)
            {
                var coneType = r.Next(0, 3);
                IPhotoReceptorCell cone = coneType switch
                {
                    0 => new LConeCell(),
                    1 => new MConeCell(),
                    2 => new SConeCell(),
                    _ => new SConeCell(),
                };

                var x = r.Next(layer.Width);
                var y = r.Next(layer.Height);
                layer[x, y] = cone;
            }
        }
    }

    // https://en.wikipedia.org/wiki/Retina#Retinal_layers
    /*
     * These layers can be grouped into 4 main processing stages:
     *  1. photoreception;
     *  2. transmission to bipolar cells;
     *  3. transmission to ganglion cells, which also contain photoreceptors, the photosensitive ganglion cells;
     *  4. transmission along the optic nerve.
     * At each synaptic stage there are also laterally
     * connecting horizontal and amacrine cells.
     * */

    /// <summary>
    /// https://en.wikipedia.org/wiki/Inner_limiting_membrane
    ///
    /// Has <seealso cref="AstrocyteCell"/> and <seealso cref="MullerGlialCell"/>.
    /// This layer is closest to center of eye, or furthest from brain.
    /// Separates the retina from the goo in the eye.
    /// </summary>
    public class InnerLimitingMembraneLayer
    {
        public InnerLimitingMembraneLayer(RetinalLayerContext c)
        {
            this.Astrocytes = new(c.Width, c.Height);
            this.NerveFiberLayer = new(c);
        }

        public Layer<AstrocyteCell> Astrocytes { get; }

        public NerveFiberLayer NerveFiberLayer { get; }
    }

    //https://en.wikipedia.org/wiki/Retinal_nerve_fiber_layer
    public class NerveFiberLayer
    {
        public NerveFiberLayer(RetinalLayerContext c) => this.GanglionCellLayer = new(c);
        // nerve fibers / axonal pattern connecting layers
        public GanglionCellLayer GanglionCellLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Ganglion_cell_layer
    public class GanglionCellLayer
    {
        // contains retinalganglioncells
        public GanglionCellLayer(RetinalLayerContext c) => this.InnerPlexiformLayer = new(c);

        public InnerPlexiformLayer InnerPlexiformLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Inner_plexiform_layer
    public class InnerPlexiformLayer
    {
        public InnerPlexiformLayer(RetinalLayerContext c) => this.InnerNuclearLayer = new(c);

        // dendrites from ganglion cell layer and inner nuclear layer
        public InnerNuclearLayer InnerNuclearLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Inner_nuclear_layer
    public class InnerNuclearLayer
    {
        // bipolar cells, horizontal cells, and amacrine cells
        // bipolar most numerous - connect to rods/cones

        // horizontal cells tons of dendrites to outerplexiformlayer
        // axons to same layer

        public InnerNuclearLayer(RetinalLayerContext c) => this.OuterPlexiformLayer = new(c);

        public OuterPlexiformLayer OuterPlexiformLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Outer_plexiform_layer
    public class OuterPlexiformLayer
    {
        // synapse layer dendrites of horizontal cells from innernuclear
        // to photo receptor cell inner segments in outernuclearlayer

        public OuterPlexiformLayer(RetinalLayerContext c) => this.OuterNuclearLayer = new(c);

        public OuterNuclearLayer OuterNuclearLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Outer_nuclear_layer
    public class OuterNuclearLayer
    {
        // part that detects light
        // rod and cone granules
        public OuterNuclearLayer(RetinalLayerContext c) => this.ExternalLimitingMembraneLayer = new(c);
        public ExternalLimitingMembraneLayer ExternalLimitingMembraneLayer { get; }
    }

    // https://en.wikipedia.org/wiki/External_limiting_membrane
    public class ExternalLimitingMembraneLayer
    {
        // membrane separating rod/cone receptor's from their nucleuses
        public ExternalLimitingMembraneLayer(RetinalLayerContext c) => this.LayerOfRodsAndConesLayer = new(c);

        public LayerOfRodsAndConesLayer LayerOfRodsAndConesLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Layer_of_rods_and_cones
    public class LayerOfRodsAndConesLayer
    {
        // layer of rod and cone receptors
        // Roc/Cones face towards the brain, and are nuzzled in to the pigments
        /// in the <seealso cref="RetinalPigmentEpitheliumLayer"/>;
        public LayerOfRodsAndConesLayer(RetinalLayerContext c) => this.RetinalPigmentEpitheliumLayer = new(c);

        public RetinalPigmentEpitheliumLayer RetinalPigmentEpitheliumLayer { get; }
    }

    // https://en.wikipedia.org/wiki/Retinal_pigment_epithelium
    public class RetinalPigmentEpitheliumLayer
    {
        // hexagonal cells of pigment
        // tons of stuff for health of eye
        // This layer is closest to the brain, or back of the eye.
        public RetinalPigmentEpitheliumLayer(RetinalLayerContext c)
        {
        }
    }

    public record RetinalLayerContext(int Width, int Height, Retina Retina);
}
