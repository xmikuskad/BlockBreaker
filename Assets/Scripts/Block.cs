using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameStatus gameStatus;

    // state variables
    [SerializeField] int receivedHits = 0; // serialized for debug

    private void Start()
    {
        CountBreakableBlocks();
        maxHits = hitSprites.Length + 1;
    }

    private void CountBreakableBlocks()
    {
        if (gameObject.tag == "Breakable")
        {
            level = FindObjectOfType<Level>();
            level.CountBlocks();
            gameStatus = FindObjectOfType<GameStatus>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        receivedHits++;
        if (receivedHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = receivedHits - 1;

        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError(gameObject.name + ": Block sprite is missing from array");
    }

    private void DestroyBlock()
    {
        level.BlockDestroyed();
        TriggerSparklesVFX();
        PlayBlockDestroySFX();
        Destroy(gameObject);
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        gameStatus.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
