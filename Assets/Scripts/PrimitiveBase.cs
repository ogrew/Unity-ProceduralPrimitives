using UnityEngine;
using PrimitiveGenerator;

public class PrimitiveBase : MonoBehaviour
{
        public void Set(Vector3? position = null, Vector3? scale = null, Vector3? rotation = null)
        {
            if(position != null)
            {
                SetPosition((Vector3)position);
            }

            if(scale != null)
            {
                SetScale((Vector3)scale);
            }

            if(rotation != null)
            {
                SetRotation((Vector3)rotation);
            }
        }

        private void SetPosition(Vector3 position)
        {
            this.gameObject.transform.localPosition = position;
        }

        private void SetScale(Vector3 scale)
        {
            this.gameObject.transform.localScale = scale;
        }

        private void SetRotation(Vector3 rotation)
        {
            this.gameObject.transform.eulerAngles = rotation;
        }

        public void SetMaterial(Material material, MeshRenderer renderer)
        {
            renderer.material = material;
        }
}
