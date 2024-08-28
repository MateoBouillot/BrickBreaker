
namespace BrickBreaker.GameObjects
{
    public class GameObject
    {
        private bool _enable = true;
        public bool isFree = false;

        public bool enable
        {
            get { return _enable; }
            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    if (_enable) OnEnable();
                    else OnDisable();
                }
            }
        }

        public GameObject(bool enable = true)
        {
            this.enable = enable;

        }

        public virtual void Update(float dt) { }

        public virtual void Draw() { }

        public virtual void OnEnable() { }

        public virtual void OnDisable() { }
    }
}
