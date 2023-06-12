namespace Helpers
{
    public static class Constants
    {
        // Sound Settings
        public const string MusicVolumePrefName = "musicVolume";
        public const string SoundVolumePrefName = "soundVolume";
        
        // Animation States
        public const string AttackTriggerName = "attack";
        public const string ShootTriggerName = "shoot";
        public const string GroundedBoolName = "grounded";
        public const string IsJumpingTriggerName = "isJumping";

        public const string MovementBoolName = "moving";
        public const string MeleeAttackTriggerName = "meleeAttack";
        public const string HurtTriggerName = "hurt";
        public const string DeathTriggerName = "die";
        
        // Collectibles
        public const int CoinValue = 50;
        public const int HealthValue = 1;
        
        // Projectile
        public const float ProjectileSpeed = 20f;
        public const float ProjectileDamageAmount = 1f;
        public const float ProjectileLifetimeValue = 2f;
    }
}