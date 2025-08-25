using System;
using UnityEngine;

namespace Chonker.Scripts.Enemy
{
    public class EnemyAiView : MonoBehaviour
    {
        private EnemyAiController enemyAiController;

        private void Awake() {
            enemyAiController = GetComponentInParent<EnemyAiController>();
        }
    }
}