import React from 'react';
import {Game, Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";
import styled from "styled-components";

export type Params = {
  player1: Player | null;
  player2: Player | null;
  onComplete: (game: Game) => void;
  onCancel: () => void;
}

const OuterContainer = styled.div`
  color: white
`

const PlayerContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  position: fixed;
  top: 0;
  height: 100%;
  width: 50%;
  text-align: center;
  transition: all 0.5s ease;
`

const Player1Container = styled(PlayerContainer)<{show: boolean}>`
  left: ${p => p.show ? "0" : "-50%"};
  background-color: darkred;
`

const Player2Container = styled(PlayerContainer)<{show: boolean}>`
  right: ${p => p.show ? "0" : "-50%"};
  background-color: darkblue;
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
  color: white;
  line-height: 30px;
  font-size: 30px;
  background: none!important;
  border: none;
  padding: 0!important;
  cursor: pointer;
`

export const WinnerSelect = ({ player1, player2, onComplete, onCancel }: Params) => {
  const inMatch = !!(player1 && player2);
  return (<OuterContainer>
      <Player1Container show={inMatch}>
          { player1 ? 
          <PlayerButton player={player1} number={1}
                        onClick={() => onComplete({winner: player1, loser: player2!})}/> : null }
      </Player1Container>
      <Player2Container show={inMatch}>
          { player2 ?
              <PlayerButton player={player2} number={2}
                            onClick={() => onComplete({winner: player2, loser: player1!})}/> : null }
      </Player2Container>
      <VSymbol show={inMatch}>V</VSymbol>
      <CancelButton show={inMatch} onClick={onCancel}>Cancel</CancelButton>
    </OuterContainer>
  );
}
