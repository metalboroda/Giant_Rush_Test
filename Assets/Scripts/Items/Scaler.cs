using Assets.Scripts.Character;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField]
    private List<Material> materials = new List<Material>();

    [SerializeField]
    private ColorState colorState;
    [SerializeField]
    private bool randomColor = true;

    private enum ColorState
    {
        ColorA,
        ColorB
    }

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        RandomizeScalerType();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var charColor = other.GetComponent<CharacterColor>();
            var charScaler = other.GetComponent<CharacterHandler>();

            if ((int)colorState == (int)charColor.characterColorState)
            {
                charScaler.IncreaseScale();

                Destroy(gameObject);
            }
            else
            {
                charScaler.DecreaseScale();

                Destroy(gameObject);
            }
        }
    }

    private void RandomizeScalerType()
    {
        if (!randomColor) return;

        var rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                colorState = ColorState.ColorA;
                skinnedMeshRenderer.material = materials[0];
                break;
            case 1:
                colorState = ColorState.ColorB;
                skinnedMeshRenderer.material = materials[1];
                break;
        }
    }
}