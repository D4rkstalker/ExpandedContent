﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using ExpandedContent.Config;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Domains {
    internal class RageDomain {

        public static void AddRageDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var DestructionDomainBaseAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("e69898f762453514780eb5e467694bdb");
            var DestructionDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("9acceeadcc1538544ac5176eb168b4a1");
            var RageFeature = Resources.GetBlueprint<BlueprintFeature>("2479395977cfeeb46b482bc3385f4647");
            var RagePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("28710502f46848d48b3f0d6132817c4e");
            var SacredWeaponEnchantAnarchicChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("6fdc32d0af41ffb42b8285dbac9a050a");

            var RageResource = Resources.GetBlueprint<BlueprintAbilityResource>("24353fcf8096ea54684a72bf58dedbc9");

            var RageDomainExtraRage = Helpers.CreateBlueprint<BlueprintFeature>("RageDomainExtraRage", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = RageResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 20;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });

            //Spelllist
            var TrueStrikeSpell = Resources.GetBlueprint<BlueprintAbility>("2c38da66e5a599347ac95b3294acbe00");
            var BullsStrengthSpell = Resources.GetBlueprint<BlueprintAbility>("4c3d08935262b6544ae97599b3a9556d");
            var RageSpell = Resources.GetBlueprint<BlueprintAbility>("97b991256e43bb140b263c326f690ce2");
            var FearSpell = Resources.GetBlueprint<BlueprintAbility>("d2aeac47450c76347aebbc02e4f463e0");
            var BoneshatterSpell = Resources.GetBlueprint<BlueprintAbility>("f2f1efac32ea2884e84ecaf14657298b");
            var HarmSpell = Resources.GetBlueprint<BlueprintAbility>("cc09224ecc9af79449816c45bc5be218");
            var DisintegrateSpell = Resources.GetBlueprint<BlueprintAbility>("4aa7942c3e62a164387a73184bca3fc1");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var TsunamiSpell = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac");
            var RageDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("RageDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TrueStrikeSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BullsStrengthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            RageSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FearSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BoneshatterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HarmSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DisintegrateSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HorridWiltingSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TsunamiSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var RageDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("RageDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RageDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var RageDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("RageDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DestructionDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = DestructionDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DestructionDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = RageDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rage Subdomain");
                bp.SetDescription("\nYou are granted a fraction of the pure divine rage of your deity.\nDestructive Smite: You gain the the {g|Encyclopedia:Special_Abilities}supernatural ability{/g} " +
                    "to make a {g|Encyclopedia:MeleeAttack}melee attack{/g} with a morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Damage}damage rolls{/g} equal to 1/2 your level in the class " +
                    "that gave you access to this domain (minimum 1).\nRage: At 8th level, you can enter a fearsome rage, like a barbarian, for a number of rounds per day equal to your cleric level + your " +
                    "constitution modifier. At 12th and 16th level, you can select one rage power. You cannot select any rage power that possesses a level requirement, but otherwise your barbarian " +
                    "level is equal to 1/2 your cleric level. These rounds of rage stack with any rounds of rage you might have from levels of barbarian.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var RageDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("RageDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = RageDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var RageDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("RageDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RageDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RageDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RageDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rage Subdomain");
                bp.SetDescription("\nYou are granted a fraction of the pure divine rage of your deity.\nDestructive Smite: You gain the the {g|Encyclopedia:Special_Abilities}supernatural ability{/g} " +
                    "to make a {g|Encyclopedia:MeleeAttack}melee attack{/g} with a morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Damage}damage rolls{/g} equal to 1/2 your level in the class " +
                    "that gave you access to this domain (minimum 1).\nRage: At 8th level, you can enter a fearsome rage, like a barbarian, for a number of rounds per day equal to your cleric level + your " +
                    "constitution modifier. At 12th and 16th level, you can select one rage power. You cannot select any rage power that possesses a level requirement, but otherwise your barbarian " +
                    "level is equal to 1/2 your cleric level. These rounds of rage stack with any rounds of rage you might have from levels of barbarian.\nDomain {g|Encyclopedia:Spell}Spells{/g}: " +
                    "true strike, boneshaker, rage, fear, boneshatter, harm, disintegrate, horrid wilting, tsunami.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.Domain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RageDomainBaseFeature),
                    Helpers.LevelEntry(8, RageFeature, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage),
                    Helpers.LevelEntry(9, RageDomainExtraRage),
                    Helpers.LevelEntry(10, RageDomainExtraRage),
                    Helpers.LevelEntry(11, RageDomainExtraRage),
                    Helpers.LevelEntry(12, RagePowerSelection, RageDomainExtraRage),
                    Helpers.LevelEntry(13, RageDomainExtraRage),
                    Helpers.LevelEntry(14, RageDomainExtraRage),
                    Helpers.LevelEntry(15, RageDomainExtraRage),
                    Helpers.LevelEntry(16, RagePowerSelection, RageDomainExtraRage),
                    Helpers.LevelEntry(16, RageDomainExtraRage),
                    Helpers.LevelEntry(17, RageDomainExtraRage),
                    Helpers.LevelEntry(18, RageDomainExtraRage),
                    Helpers.LevelEntry(19, RageDomainExtraRage),
                    Helpers.LevelEntry(20, RageDomainExtraRage)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RageDomainBaseFeature, RageFeature, RagePowerSelection, RagePowerSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var RageDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("RageDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RageDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RageDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Rage Subdomain");
                bp.SetDescription("\nYou are granted a fraction of the pure divine rage of your deity.\nDestructive Smite: You gain the the {g|Encyclopedia:Special_Abilities}supernatural ability{/g} " +
                    "to make a {g|Encyclopedia:MeleeAttack}melee attack{/g} with a morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Damage}damage rolls{/g} equal to 1/2 your level in the class " +
                    "that gave you access to this domain (minimum 1).\nRage: At 8th level, you can enter a fearsome rage, like a barbarian, for a number of rounds per day equal to your cleric level + your " +
                    "constitution modifier. At 12th and 16th level, you can select one rage power. You cannot select any rage power that possesses a level requirement, but otherwise your barbarian " +
                    "level is equal to 1/2 your cleric level. These rounds of rage stack with any rounds of rage you might have from levels of barbarian.\nDomain {g|Encyclopedia:Spell}Spells{/g}: " +
                    "true strike, boneshaker, rage, fear, boneshatter, harm, disintegrate, horrid wilting, tsunami.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RageDomainBaseFeature),
                    Helpers.LevelEntry(8, RageFeature, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage, RageDomainExtraRage),
                    Helpers.LevelEntry(9, RageDomainExtraRage),
                    Helpers.LevelEntry(10, RageDomainExtraRage),
                    Helpers.LevelEntry(11, RageDomainExtraRage),
                    Helpers.LevelEntry(12, RagePowerSelection, RageDomainExtraRage),
                    Helpers.LevelEntry(13, RageDomainExtraRage),
                    Helpers.LevelEntry(14, RageDomainExtraRage),
                    Helpers.LevelEntry(15, RageDomainExtraRage),
                    Helpers.LevelEntry(16, RagePowerSelection, RageDomainExtraRage),
                    Helpers.LevelEntry(16, RageDomainExtraRage),
                    Helpers.LevelEntry(17, RageDomainExtraRage),
                    Helpers.LevelEntry(18, RageDomainExtraRage),
                    Helpers.LevelEntry(19, RageDomainExtraRage),
                    Helpers.LevelEntry(20, RageDomainExtraRage)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RageDomainBaseFeature, RageFeature, RagePowerSelection, RagePowerSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            RageDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                RageDomainProgression.ToReference<BlueprintFeatureReference>(),
                RageDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            RageDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RageDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            RageDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = RageDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            if (ModSettings.AddedContent.Domains.IsDisabled("Rage Subdomain")) { return; }
            DomainTools.RegisterDomain(RageDomainProgression);
            DomainTools.RegisterSecondaryDomain(RageDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(RageDomainProgression);
            DomainTools.RegisterTempleDomain(RageDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(RageDomainProgressionSecondary);
           
        }

    }
}
