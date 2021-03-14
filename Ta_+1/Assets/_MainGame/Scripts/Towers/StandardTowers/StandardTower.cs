namespace Tut.Scripts.Towers.StandardTowers
{
    public class StandardTower : Tower
    {
        private void Start()
        {
            TowerType = TowerType.Standard;
        }

        public override Debuff GetDebuff()
        {
            return new StandardDebuff(Target, DebuffDuration);
        }
    }
}