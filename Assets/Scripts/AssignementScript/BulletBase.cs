using UnityEngine;
public abstract class BulletBase<T> : MonoBehaviour where T: MonoBehaviour
{
    
    public virtual void Damage() { }

    public virtual void BulletMoves() {

    }

}