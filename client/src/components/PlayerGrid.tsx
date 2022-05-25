import React from 'react';
import {Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";

export type Params = {
  players: Player[]
}

export const PlayerGrid = ({ players }: Params) => {
  return (
    <div>
      { players.map(player => <PlayerButton player={player} key={player.id}/>) }
    </div>
  );
}
