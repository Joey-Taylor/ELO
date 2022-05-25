import React from 'react';
import {Player} from "../models/Player";

export type Params = {
  player: Player
}

export const PlayerButton = ({player}: Params) => {
  return (
      <div>
          <div>{player.rank}</div>
          <img src={player.image} />
          <div>{player.name}</div>
          <div>{player.rating}</div>
      </div>
  );
}
