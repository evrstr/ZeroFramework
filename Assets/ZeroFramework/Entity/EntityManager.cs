

namespace ZeroFramework.Entity
{
    public class EntityMgr : SingletonBase<EntityMgr>, IEntityManager
    {
        public int EntityCount => throw new System.NotImplementedException();

        public int EntityGroupCount => throw new System.NotImplementedException();

        protected override void OnInit()
        {

        }
    }
}