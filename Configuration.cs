using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using Newtonsoft.Json;

namespace OopsAllLalafells {
    public class Configuration : IPluginConfiguration {
        [NonSerialized]
        private DalamudPluginInterface pluginInterface;

        public int Version { get; set; } = 1;

        public Race? ChangeOthersSourceRace { get; set; } = null;

        public Race ChangeOthersTargetRace { get; set; } = Race.LALAFELL;
        
        public bool ShouldChangeOthers { get; set; } = false;
        
        public void Initialize(DalamudPluginInterface pluginInterface) {
            this.pluginInterface = pluginInterface;
        }

        public void Save() {
            this.pluginInterface.SavePluginConfig(this);
        }
    }
}
