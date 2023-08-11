using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPcounter : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player playerHealth;
    List<HPControl> hearts = new List<HPControl>();

    private void OnEnable()
    {
        Player.OnPlayerDamaged += DrawHearts;
        Player.OnPlayerHealed += DrawHearts;
    }

    private void OnDisable()
    {
        Player.OnPlayerDamaged -= DrawHearts;
        Player.OnPlayerHealed -= DrawHearts;
    }

    void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = playerHealth.PlayerHp % 2;
        int heartsToMake = (int) ((playerHealth.PlayerHp / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.PlayerHp - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HPControl heartComponent = newHeart.GetComponent<HPControl>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HPControl>();
    }
}
