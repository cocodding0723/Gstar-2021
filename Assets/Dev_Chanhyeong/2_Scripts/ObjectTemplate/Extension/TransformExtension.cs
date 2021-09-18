using UnityEngine;

namespace ObjectTemplate.Extention
{
    public static class TransformExtension
    {
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.zero;
        }

        public static void ResetLocalTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.zero;
        }

        public static void SetPositionX(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(value, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        public static void SetPositionY(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(transform.position.x, value, transform.position.z);
            transform.position = newPosition;
        }

        public static void SetPositionZ(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, value);
            transform.position = newPosition;
        }

        public static void SetLocalPositionX(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(value, transform.position.y, transform.position.z);
            transform.localPosition = newPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(transform.position.x, value, transform.position.z);
            transform.localPosition = newPosition;
        }

        public static void SetLocalPositionZ(this Transform transform, float value)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, value);
            transform.localPosition = newPosition;
        }

        public static void SetRotationX(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(value, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Euler(newRotation);
        }

        public static void SetRotationY(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(transform.position.x, value, transform.position.z);
            transform.rotation = Quaternion.Euler(newRotation);
        }

        public static void SetRotationZ(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(transform.position.x, transform.position.y, value);
            transform.rotation = Quaternion.Euler(newRotation);
        }

        public static void SetLocalRotationX(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(value, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Euler(newRotation);
        }

        public static void SetLocalRotationY(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(transform.position.x, value, transform.position.z);
            transform.localRotation = Quaternion.Euler(newRotation);
        }

        public static void SetLocalRotationZ(this Transform transform, float value)
        {
            Vector3 newRotation = new Vector3(transform.position.x, transform.position.y, value);
            transform.localRotation = Quaternion.Euler(newRotation);
        }

        public static void SetLocalScaleX(this Transform transform, float value)
        {
            Vector3 newScale = new Vector3(value, transform.lossyScale.y, transform.lossyScale.z);
            transform.localScale = newScale;
        }

        public static void SetLocalScaleY(this Transform transform, float value)
        {
            Vector3 newScale = new Vector3(transform.lossyScale.x, value, transform.lossyScale.z);
            transform.localScale = newScale;
        }

        public static void SetLocalScaleZ(this Transform transform, float value)
        {
            Vector3 newScale = new Vector3(transform.lossyScale.x, transform.lossyScale.y, value);
            transform.localScale = newScale;
        }
    }
}