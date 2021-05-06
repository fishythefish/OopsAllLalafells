using ImGuiNET;
using System;
using System.Numerics;
using Dalamud.Game.Text;

namespace OopsAllLalafells
{
    public class PluginUI
    {
        private readonly Plugin plugin;

        public PluginUI(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void Draw()
        {
            if (!this.plugin.SettingsVisible)
            {
                return;
            }

            bool settingsVisible = this.plugin.SettingsVisible;
            if (ImGui.Begin("Oops, All Lalafells!", ref settingsVisible, ImGuiWindowFlags.AlwaysAutoResize))
            {
                bool shouldChangeOthers = this.plugin.config.ShouldChangeOthers;
                ImGui.Checkbox("Change other players", ref shouldChangeOthers);
                
                Race? othersSourceRace = this.plugin.config.ChangeOthersSourceRace;
                Race othersTargetRace = this.plugin.config.ChangeOthersTargetRace;
                if (shouldChangeOthers)
                {
                    String currentSourceRace = othersSourceRace.HasValue ? othersSourceRace.Value.GetAttribute<Display>().Value : "Any";
                    if (ImGui.BeginCombo("Original Race", currentSourceRace))
                    {
                        ImGui.PushID(0);
                        if (ImGui.Selectable("Any", !othersSourceRace.HasValue))
                        {
                            othersSourceRace = null;
                        }

                        if (!othersSourceRace.HasValue)
                        {
                            ImGui.SetItemDefaultFocus();
                        }
                        ImGui.PopID();

                        foreach (Race race in Enum.GetValues(typeof(Race)))
                        {
                            ImGui.PushID((byte)race);
                            if (ImGui.Selectable(race.GetAttribute<Display>().Value, race == othersSourceRace))
                            {
                                othersSourceRace = race;
                            }

                            if (race == othersSourceRace)
                            {
                                ImGui.SetItemDefaultFocus();
                            }

                            ImGui.PopID();
                        }

                        ImGui.EndCombo();
                    }

                    if (ImGui.BeginCombo("New Race", othersTargetRace.GetAttribute<Display>().Value))
                    {
                        foreach (Race race in Enum.GetValues(typeof(Race)))
                        {
                            ImGui.PushID((byte) race);
                            if (ImGui.Selectable(race.GetAttribute<Display>().Value, race == othersTargetRace))
                            {
                                othersTargetRace = race;
                            }

                            if (race == othersTargetRace)
                            {
                                ImGui.SetItemDefaultFocus();
                            }

                            ImGui.PopID();
                        }

                        ImGui.EndCombo();
                    }
                }

                this.plugin.UpdateOtherSourceRace(othersSourceRace);
                this.plugin.UpdateOtherTargetRace(othersTargetRace);
                
                this.plugin.ToggleOtherRace(shouldChangeOthers);

                ImGui.End();
            }

            this.plugin.SettingsVisible = settingsVisible;
            this.plugin.SaveConfig();
        }
    }
}