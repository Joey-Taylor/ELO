import React from 'react';
import {Player} from "../models/Player";
import styled from "styled-components";

export type Params = {
  player: Player
}

const Container = styled.div`
    text-align: center;
    width: 130px;
    padding: 10px;
    display: inline-block;
`

const ImageAndRank = styled.div`
    position: relative;
    width: 130px;
    height: 130px;
    padding-bottom: 10px;
`
type RankProps = {
    rank: number;
}
const backgroundForRank = (rank: number) => {
    switch(rank) {
        case 1:
            return "gold";
        case 2:
            return "silver";
        case 3:
            return "#CD7F32";
        default: 
            return "red";
    }
}
const Rank = styled.div<RankProps>`
    position: absolute;
    background: ${p => backgroundForRank(p.rank)};
    border-radius: 50%;
    color: white;
    width: 23%;
    height: 23%;
    text-align: center;
    line-height: 30px;
`


const Image = styled.img`
    border-radius: 50%;
    width: 100%;
    height: 100%;
`

const Name = styled.div`
    text-align: center;
    font-size: large;
`
const Rating = styled.div`
    text-align: center;
    color: #555
`

export const PlayerButton = ({player}: Params) => {
  return (
      <Container>
          <ImageAndRank>
              <Rank rank={player.rank}>{player.rank}</Rank>
              <Image src={player.image} alt={player.name} />
          </ImageAndRank>
          <Name>{player.name}</Name>
          <Rating>{player.rating}</Rating>
      </Container>
  );
}
