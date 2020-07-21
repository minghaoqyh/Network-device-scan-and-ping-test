using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service_tool
{
    public class Features
    {
        //Feature ID = 0x00
        public int GreenLightMode { get; set; }
        public string GreenLightModeValidDate { get; set; }
        //Feature ID = 0x01
        public int OrangecolorMode { get; set; }
        public string OrangecolorModeValidDate { get; set; }
        //Feature ID = 0x02
        public int TrueLightMode { get; set; }
        public string TrueLightModeValidDate { get; set; }
        //Feature ID = 0x03
        public int FluorescenceMode { get; set; }
        public string FluorescenceModeValidDate { get; set; }
        //Feature ID = 0x04
        public int NoGlareMode { get; set; }
        public string NoGlareModeValidDate { get; set; }
        //Feature ID = 0x05
        public int LightBoost { get; set; }
        public string LightBoostValidDate { get; set; }
        //Feature ID = 0x06
        public int MultispectralMode { get; set; }
        public string MultispectralModeValidDate { get; set; }
        //Feature ID = 0x07
        public int DICOMConnectivity { get; set; }
        public string DICOMConnectivityValidDate { get; set; }

        public int VioletMode { get; set; }
        public string VioletModeValidDate { get; set; }
    }

    public enum FeatureID
    {
        GreenLightMode,
        orangeColorMode,
        TrueLightMode,
        FluorescenceMode,
        NoGlareMode,
        LightBoost,
        MultispectralMode,
        DICOMConnectivity
    }
}
