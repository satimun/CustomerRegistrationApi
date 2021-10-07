using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegistrationAPI.Constant
{
    public class StaticValue
    {
        private bool _IsDevMode;
        public bool IsDevMode { get => _IsDevMode; }

        private static StaticValue instant { get; set; }

        public static StaticValue GetInstant()
        {
            if (instant == null) instant = new StaticValue();
            return instant;
        }

        public void SetDevMode()
        {
            _IsDevMode = true;
        }   

        private string _ProductionPlanningUrl;
        public string ProductionPlaningUrl { get => _ProductionPlanningUrl; }
        public void SetProductionPlanningUrl(string url)
        {
            _ProductionPlanningUrl = url;
        }

        private string _SingleSignOnUrl;
        public string SingleSignOnUrl { get => _SingleSignOnUrl; }
        public void SetSingleSignOnUrl(string url)
        {
            _SingleSignOnUrl = url;
        }

        private string _MasterDataUrl;
        public string MasterDataUrl { get => _MasterDataUrl; }
        public void SetMasterDataUrl(string url)
        {
            _MasterDataUrl = url;
        }
    }
}
