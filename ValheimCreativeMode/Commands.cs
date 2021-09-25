namespace ValheimCreativeMode
{
    public class Commands
    {
        public bool cheatsEnabled = false;
        public bool debugModeEnabled = false;
        public bool damageCheatEnabled = false;
        public bool staminaCheatEnabled = false;
        public bool wearNTearCheatEnabled = false;
        public bool dropItemsCheatEnabled = false;
        public bool fireplaceCheatEnabled = false;

        public bool isServer;

        public void ToggleCheats()
        {
            cheatsEnabled = !cheatsEnabled;
        }

        public void ToggleDebugMode()
        {
            debugModeEnabled = !debugModeEnabled;
        }
        public void ToggleDamageCheat()
        {
            damageCheatEnabled = !damageCheatEnabled;
        }

        public void ToggleStaminaCheat()
        {
            staminaCheatEnabled = !staminaCheatEnabled;
        }

        public void ToggleWearNTearCheat()
        {
            wearNTearCheatEnabled = !wearNTearCheatEnabled;
        }

        public void ToggleDropItemsCheat()
        {
            dropItemsCheatEnabled = !dropItemsCheatEnabled;
        }

        public void ToggleFireplaceCheat()
        {
            fireplaceCheatEnabled = !fireplaceCheatEnabled;
        }
    }
}
