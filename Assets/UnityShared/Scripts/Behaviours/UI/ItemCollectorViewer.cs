using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Behaviours.UI
{
    public class ItemCollectorViewer : MonoBehaviour
    {
        public GameObject itemTemplate;
        public Creator creator;
        public Collector collector;

        public void Run() => StartCoroutine(RunCO());
        private IEnumerator RunCO()
        {
            // Instancia los objetos del UI
            var lstItems = new List<GameObject>();
            for (int i = 0; i < creator.count; i++)
            {
                var item = Instantiate(itemTemplate, transform);

                var angle = (float)UnityEngine.Random.Range(0, (float)Math.PI * 2);
                var rad = UnityEngine.Random.Range(0, creator.radius);
                item.transform.localPosition = new Vector3((float)Math.Cos(angle), (float)Math.Sin(angle), 0) * rad;

                lstItems.Add(item);
            }

            // Activa los objetos lentamente
            float _timer = 0;
            while (_timer < creator.time)
            {
                _timer += Time.deltaTime;
                float index = (_timer / creator.time) * (float)creator.count;
                lstItems.Take((int)index).Where(item => !item.activeSelf).ToList().ForEach(x => x.SetActive(true));
                yield return null;
            }

            // demora antes de la recoleccion
            yield return new WaitForSeconds(collector.delayTime);

            // trasladad los elementos a la posicion objetivo
            var lstDistance = lstItems.Select(item => Vector3.Distance(collector.Target.position, item.transform.position)).ToList();
            var lstTimeToReach = lstItems.Select(item => UnityEngine.Random.Range(collector.timeToReach.Min, collector.timeToReach.Max)).ToList();
            while (lstItems.Any(item => Math.Abs((item.transform.position - collector.Target.position).magnitude) > 0.01f))
            {
                for (int i = 0; i < lstItems.Count; i++)
                    lstItems[i].transform.position = Vector2.MoveTowards(lstItems[i].transform.position, collector.Target.position, (lstDistance[i] * Time.deltaTime) / lstTimeToReach[i]);

                yield return null;
            }

            if (collector.destroy)
                lstItems.ForEach(item => Destroy(item));
        }

        [Serializable]
        public class Creator
        {
            public int count;
            public float time;
            public float radius;
        }

        [Serializable]
        public class Collector
        {
            public float delayTime;
            public Transform Target;
            public RangeNumber<float> timeToReach;
            public bool destroy;
        }
    }
}