using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI turnText;
    public TMPro.TextMeshProUGUI rollText;
    public Player[] players;
    public Tile[] tiles;

    public Button RollDiceButton;

    int currentPlayer = 0;
    int roll = 0;
    bool climbedLadder = false;

    public void RollDice()
    {
        climbedLadder = false;
        RollDiceButton.interactable = false;
        roll = Random.Range(1, 7);
        //todo: animate dice roll
        rollText.text = "Player " + (currentPlayer + 1) + " rolled " + roll.ToString();
        if (players[currentPlayer].position == 0 && roll != 1)
        {
            rollText.text += "\n(Roll 1 to enter the game)";
            currentPlayer = (currentPlayer + 1) % players.Length;
            if (currentPlayer == 0) RollDiceButton.interactable = true;
            else RollDice(); // System Plays
            return;
        }
        MovePlayer(roll);
    }



    void MovePlayer(int amount)
    {
        players[currentPlayer].position += amount;

        if (players[currentPlayer].position >= tiles.Length)
        {
            players[currentPlayer].transform.DOMove(tiles[tiles.Length - 1].transform.position, 1f);
            rollText.text = "End of the game, player " + (currentPlayer + 1) + " wins!";
            RollDiceButton.interactable = false;
            return;
        }

        players[currentPlayer].transform.DOMove(tiles[players[currentPlayer].position - 1].transform.position, 1f);
        Invoke("CheckSnakeAndLadder", 1f);
    }

    void CheckSnakeAndLadder()
    {
        float animTime = 0f;
        if (tiles[players[currentPlayer].position - 1].snakeTo != null)
        {
            players[currentPlayer].position = tiles[players[currentPlayer].position - 1].snakeTo.Number;
            players[currentPlayer].transform.DOMove(tiles[players[currentPlayer].position - 1].transform.position, 1f);
            animTime += 1f;
        }
        else
        {
            if (tiles[players[currentPlayer].position - 1].ladderTo != null)
            {
                players[currentPlayer].position = tiles[players[currentPlayer].position - 1].ladderTo.Number;
                players[currentPlayer].transform.DOMove(tiles[players[currentPlayer].position - 1].transform.position, 1f);
                animTime += 1f;
                climbedLadder = true;
            }
        }
        Invoke("SwapTurn", animTime);
    }

    void SwapTurn()
    {
        if (roll == 6)
        {
            turnText.text = "Rolled 6! Player " + (currentPlayer + 1) + " gets another turn!";
        }
        else if (climbedLadder)
        {
            turnText.text = "Up the Ladder! Player " + (currentPlayer + 1) + " gets another turn!";
        }
        else
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            turnText.text = "Player " + (currentPlayer + 1) + "'s turn";
        }
        if (currentPlayer == 0) RollDiceButton.interactable = true;
        else RollDice(); // System Plays
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
