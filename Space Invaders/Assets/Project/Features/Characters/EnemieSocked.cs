using System;
using System.Collections;
using UnityEngine;

namespace Game.Characters
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class EnemieSocked : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Sprite _sprite1;
        private Sprite _sprite2;

        public event Action<EnemieSocked> OnDestroyed;

        public void Setup(Sprite p_sprite1, Sprite p_sprite2)
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();

            _sprite1 = p_sprite1;
            _sprite2 = p_sprite2;
            _spriteRenderer.sprite = _sprite1;
        }

        public void ChangeSprite()
        {
            _spriteRenderer.sprite = _spriteRenderer.sprite == _sprite1 ? _sprite2 : _sprite1;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
            OnDestroyed?.Invoke(this);
        }
    }
}
