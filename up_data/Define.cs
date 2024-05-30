using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace up_data {
    class Define {
        public Path path { get; set; }
        public Project project { get; set; }

        public string nameFileConfig { get; set; }
        public string namePrismConfig { get; set; }

        
        public Define() {
            path = new Path();
            project = new Project();
            nameFileConfig = "up_data_config";
            namePrismConfig = "prism_config";
        }

        public class Path {
            public string data { get; set; }
            public string data_err { get; set; }
            public string data_passed { get; set; }
            public string data_success { get; set; }

            public Path() {
                data = "D:\\DATA_MIS";
                data_err = "D:\\DATA_MIS_ERROR";
                data_passed = "D:\\DATA_MIS_PASSED";
                data_success = "D:\\DATA_MIS_SUCCESS";
            }
        }
        public class Project {
            public string denaliF1 = "Denali [FCT1]";
            public string dryIceF1 = "Dry-ice [FCT1]";
            public string sugarloafF1 = "Sugarloaf [FCT1]";
            public string denaliNextgenF1 = "Denali Nextgen [FCT1]";
            public string denaliNextgenF2 = "Denali Nextgen [FCT2]";
            public string smrF1 = "SMR [FCT1]";
            public string lppF1 = "LPP [FCT1]";
            public string valencellF1 = "Valencell [FCT1]";
            public string sollatekF1 = "Sollatek [FCT1]";
            public string sollatekF2 = "Sollatek [FCT2]";
            public string linak10SmiF1 = "Linak10SMI [FCT1]";
        }
    }
}
