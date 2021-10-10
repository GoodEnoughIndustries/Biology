using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Cells.PhotoReceptors
{
    /// <summary>
    /// Photoreceptive cell that responds to a wavelength range of light.
    ///
    /// The three types of cone cell respond (roughly) to light of
    /// short, medium, and long wavelengths, so they may respectively be referred to as
    /// S-cones, M-cones, and L-cones.
    ///
    /// Human eye has roughly 6 million cone cells
    /// </summary>
    public class ConeCell : Cell, IPhotoReceptorCell
    {
    }

    public class SConeCell : ConeCell { }
    public class MConeCell : ConeCell { }
    public class LConeCell : ConeCell { }
}
