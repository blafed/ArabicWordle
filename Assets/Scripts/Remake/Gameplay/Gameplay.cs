using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Run the game, updated its state, and perform losing, winning, or any other result 
/// </summary>
public class Gameplay : MonoBehaviour
{
     IMode mode { get; set; }
     IState state { get; set; }
     IStateChanger stateChanger { get; set; }
     ITerminate terminate { get; set; }
     ISupply supply { get; set; }
    public GameResult result { get; set; }


    private void Start()
    {
        mode = GetComponentInChildren<IMode>();
        state = GetComponentInChildren<IState>();
        supply = GetComponentInChildren<ISupply>();
        terminate = GetComponentInChildren<ITerminate>();
        stateChanger = GetComponentInChildren<IStateChanger>();
    }

    private void Update()
    {
        if (state == null)
        {
            state = mode.initialState;
        }
        var next = stateChanger.GetNext(state);
        if (mode.CanMoveNext(state, next))
            state = next;

        foreach (var x in next.productEffects)
        {
            supply.Affect(x);
        }

        var result = mode.GetResult(state);
        if (this.result != result)
        {
            this.result = result;
            terminate.ApplyResult(result);
        }
    }
}


public interface IStateChanger
{
    IState GetNext(IState state);
}
public interface IMode
{
    IState initialState { get; }
    ModeCode code { get; }
    /// <summary>
    /// Is the next state valid, to move to?
    /// </summary>
    /// <param name="state">current game state</param>
    /// <param name="nextState">next game state</param>
    /// <returns></returns>
    bool CanMoveNext(IState state, IState nextState);
    /// <summary>
    /// What current game state indicates: winning, losing or nothing
    /// </summary>
    /// <param name="state">current game state</param>
    /// <returns></returns>
    GameResult GetResult(IState state);

    int GetScore(IState state, IState next);
}

public interface IState
{
    IReadOnlyList<ProductEffect> productEffects { get; }
}

public interface ITerminate
{
    void ApplyResult(GameResult result);
}

public interface ISupply
{
    void Affect(ProductEffect effect);
}



public enum ModeCode
{
    Unknown,
    Classic,
    
}

public enum GameResult
{
    None,
    Win,
    Lose,
}

