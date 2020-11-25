﻿using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Weapons {
    /**
     * @author Brenton Hauth
     * @date 11/18/20
     * <summary>
     * Handles all weapon interactions for the Player.
     * Extends <c>WeaponHandler</c>.
     * </summary>
     * <see cref="WeaponHandler" />
     */
    [RequireComponent(typeof(PlayerController))]
    public class PlayerWeaponHandler : WeaponHandler {
        #region Exposed Variables
        #endregion

        #region Variables
        private PlayerController PlayerController;
        #endregion

        #region Properties
        public bool Fire1 => Input.GetButton("Fire1");
        public override Vector3 BulletOrigin => Camera.main.transform.position;
        public override Vector3 BulletDirection => Camera.main.transform.forward;
        #endregion

        #region Methods
        protected override void Start() {
            base.Start();
            PlayerController = GetComponent<PlayerController>();
        }

        protected override void Update() {
            if (Fire1 && HasWeapon) {
                CurrentWeapon.TryAttacking();
            }
        }

        public override void OnShoot() {
            base.OnShoot();
            PlayerController.TriggerFire();
        }

        public override bool Equip(GameObject weapon) {
            if (base.Equip(weapon)) {
                HudHandler.ClearPrompt();
                return true;
            }
            return false;
        }

        public override void OnHit(GameObject o) {
            base.OnHit(o);
            if (o.tag == "Enemy") {
                var health = o.GetComponent<Health>();
                health.CurrentHP -= Math.Abs(CurrentWeapon.Damage);
            }

            print($"Hit: {o.name} [{o.tag}]");
        }
        
        #endregion
    }
}