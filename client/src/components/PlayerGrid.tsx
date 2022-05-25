import React from 'react';
import {Player} from "../models/Player";
import {PlayerButton} from "./PlayerButton";
import styled from "styled-components";

export type Params = {
  players: Player[]
}

const Container = styled.div`
`

export const PlayerGrid = ({ players }: Params) => {
  return (
    <Container>
      { players.map(player => <PlayerButton player={player} key={player.id}/>) }
    </Container>
  );
}
