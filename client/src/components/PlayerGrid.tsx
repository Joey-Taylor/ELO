import React from 'react';
import {Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";
import styled from "styled-components";

export type Params = {
  players: Player[];
  player1: Player | null;
  player2: Player | null;
  onSelectPlayer: (player: Player) => void;
}

const Container = styled.div`
  text-align: center;
`

export const PlayerGrid = ({ players, player1, player2, onSelectPlayer }: Params) => {
  const inMatch = !!(player1 && player2);
  return (
    <Container>
      { players.map(player => 
          <PlayerButton player={player} key={player.id} inMatch={inMatch}
                        number={player1 === player ? 1 : player2 == player ? 2 : null} 
                        onSelect={() => onSelectPlayer(player)}/>) }
    </Container>
  );
}
