import React, {useState} from 'react';
import {Game, Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";
import styled from "styled-components";
import {player1Background, player2Background, transparentWhite, white, winnerBackground} from "./colours";

export type Params = {
  player1: Player | null;
  player2: Player | null;
  onComplete: (game: Game) => void;
  onCancel: () => void;
}

const OuterContainer = styled.div`
  color: ${white}
`

const PlayerContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  position: fixed;
  top: 0;
  height: 100%;
  width: 50%;
  text-align: center;
  transition: background none;
  transition: all 0.5s ease;
`
// We add a pixel to ensure this doesn't leave a thin gap with resolution scaling.
const Player1Container = styled(PlayerContainer)<{show: boolean, winner: boolean}>`
  width: calc(50% + 1px);
  left: ${p => p.show ? "0" : "calc(-50% - 1px)"};
  background-color: ${p => p.winner ? winnerBackground : player1Background};
`

const Player2Container = styled(PlayerContainer)<{show: boolean, winner: boolean}>`
  right: ${p => p.show ? "0" : "-50%"};
  background-color: ${p => p.winner ? winnerBackground : player2Background};
`

const CentredDiv = styled.div<{show: boolean}>`
  position: fixed;
  opacity: ${p => p.show ? "100%": "0"};
  transition: opacity 0.5s ease;
  text-align: center;
`

const VSymbol = styled(CentredDiv)`
  position: fixed;
  top: calc(50% - 30px);
  left: calc(50% - 30px);
  width: 60px;
  height: 60px;
  line-height: 60px;
  font-size: 60px;
`

const CancelButton = styled(CentredDiv)`
  left: calc(50% - 60px);
  bottom: 30px;
  width: 120px;
  height: 30px;
  line-height: 30px;
  font-size: 30px;
  background: none!important;
  border: none;
  padding: 0!important;
  cursor: pointer;
`

const WinButton = styled.button`
  display: block;
  background: none;
  border: ${white} solid 1px;
  border-radius: 5px;
  padding: 10px !important;
  cursor: pointer;
  color: ${white};
  font-size: 30px;
  
  &:hover {
    background: ${transparentWhite};
  }
`

type PlayerAndWinButtonProps = {
    player: Player | null, show: boolean, number: 1 | 2, winner: 1 | 2 | null, onWin: () => void
}
const PlayerAndWinButton = ({ player, show, number, winner, onWin } : PlayerAndWinButtonProps) => {
    const Container = number === 1 ? Player1Container : Player2Container;
    return (
        <Container show={show} winner={winner === number}>
            { player 
                ? <>
                    <PlayerButton player={player} number={number}/> 
                    <WinButton onClick={onWin}>Winner</WinButton>
                </> 
                : null 
            }
        </Container>
        
    )
}

export const WinnerSelect = ({ player1, player2, onComplete, onCancel }: Params) => {
  const inMatch = !!(player1 && player2);
  const [lastWinner, setLastWinner] = useState<1 | 2 | null>(null);
  const winner = player1 ? null : lastWinner;
  
  const completeHandler = (game: Game) => {
      setLastWinner(game.winner === player1 ? 1 : 2);
      onComplete(game);
  }
  
  return (<OuterContainer>
      <PlayerAndWinButton player={player1} number={1} winner={winner} show={inMatch} 
                          onWin={() => completeHandler({winner: player1!, loser: player2!})}/>
      <PlayerAndWinButton player={player2} number={2} winner={winner} show={inMatch}
                          onWin={() => completeHandler({winner: player2!, loser: player1!})}/>
      <VSymbol show={inMatch}>V</VSymbol>
      <CancelButton show={inMatch} onClick={onCancel}>Cancel</CancelButton>
    </OuterContainer>
  );
}
