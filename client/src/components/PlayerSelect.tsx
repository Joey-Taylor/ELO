import React from 'react';
import {Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";
import styled from "styled-components";

export type Params = {
  players: Player[];
  selectedPlayer: Player | null;
  onSelectPlayer: (player: Player) => void;
}

const GridContainer = styled.div`
  text-align: center;
`

export const PlayerSelect = ({ players, selectedPlayer, onSelectPlayer }: Params) => {
  return (
      <GridContainer>
        { players.map(player => 
            <PlayerButton player={player} key={player.id}
                          number={player === selectedPlayer ? 1 : null}
                          onClick={() => onSelectPlayer(player)}/>) }
      </GridContainer>
  );
}
