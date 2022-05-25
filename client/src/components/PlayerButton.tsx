import React from 'react';
import {Player} from "../models/Player";
import styled from "styled-components";

type PlayerNumber = 1 | 2 | null;

export type Params = {
  player: Player;
  number: PlayerNumber;
  onClick: () => void;
}

const Container = styled.div`
    text-align: center;
    width: 110px;
    padding: 10px;
    display: inline-block;
    @media (min-width: 650px) {
        width: 175px;
        padding: 20px;
    }
`

const ImageAndRank = styled.div`
    position: relative;
    width: 110px;
    height: 110px;
    padding-bottom: 10px;
    @media (min-width: 650px) {
        width: 175px;
        height: 175px;
    }
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
            return "green";
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
    vertical-align: middle;
    display: flex;
    align-items: center;
`

const RankNumber = styled.div`
    width: 100%;
`


type ImageProps = {
    number: PlayerNumber | null;
}
const Image = styled.img<ImageProps>`
    border-radius: 50%;
    width: 100%;
    height: 100%;
    outline-width: 7px;
    outline-color: ${p => p.number === 1 ? "red" : p.number === 2 ? "blue" : "transparent"};
    outline-style: solid;
    transition: outline-color 0.5s ease;
    @media (min-width: 650px) {
        outline-width: 10px;
    }
`

const Name = styled.div`
    text-align: center;
    font-size: large;
`
const Rating = styled.div`
    text-align: center;
    color: #777
`

export const PlayerButton = ({player, number, onClick}: Params) => {
  return (
      <Container onClick={onClick}>
          <ImageAndRank>
              <Rank rank={player.rank}><RankNumber>{player.rank}</RankNumber></Rank>
              <Image src={player.image} alt={player.name} number={number} />
          </ImageAndRank>
          <Name>{player.name}</Name>
          <Rating>{player.rating}</Rating>
      </Container>
  );
}
