import {generatePlayers, Player, rankPlayers, UnrankedPlayer, updateWithGame} from "./Player";

const dummyPlayer = { name: "Dummy", image: "http://example.com" }

describe('players.ts', () => {
  describe('generatePlayers', () => {
    it('returns 5 players', () => {
      expect(generatePlayers(5).length).toBe(5);
    });
  });

  describe('rankPlayers', () => {
    it('orders players with different ratings', () => {
      const unrankedPlayers: UnrankedPlayer[] = [
        {...dummyPlayer, rating: 750, id: 1},
        {...dummyPlayer, rating: 1250, id: 2},
        {...dummyPlayer, rating: 1000, id: 3}
      ];

      const rankedPlayers = rankPlayers(unrankedPlayers);
      const expected: Player[] = [
        {...unrankedPlayers[1], rank: 1},
        {...unrankedPlayers[2], rank: 2},
        {...unrankedPlayers[0], rank: 3}
      ]
      expect(rankedPlayers).toStrictEqual(expected);
    });

    it('orders players with two in first place, gives them rank 1 then moves on to 3', () => {
      const unrankedPlayers: UnrankedPlayer[] = [
        {...dummyPlayer, rating: 750, id: 1},
        {...dummyPlayer, rating: 1250, id: 2},
        {...dummyPlayer, rating: 1250, id: 3}
      ];

      const rankedPlayers = rankPlayers(unrankedPlayers);
      const expected: Player[] = [
        {...unrankedPlayers[1], rank: 1},
        {...unrankedPlayers[2], rank: 1},
        {...unrankedPlayers[0], rank: 3}
      ]
      expect(rankedPlayers).toStrictEqual(expected);
    });

    it('orders players with two in second place, gives them both rank 2', () => {
      const unrankedPlayers: UnrankedPlayer[] = [
        {...dummyPlayer, rating: 1000, id: 1},
        {...dummyPlayer, rating: 1000, id: 2},
        {...dummyPlayer, rating: 1250, id: 3}
      ];

      const rankedPlayers = rankPlayers(unrankedPlayers);
      const expected: Player[] = [
        {...unrankedPlayers[2], rank: 1},
        {...unrankedPlayers[0], rank: 2},
        {...unrankedPlayers[1], rank: 2}
      ]
      expect(rankedPlayers).toStrictEqual(expected);
    });
  })

  describe('updateWithGame', () => {
    it('updates points and re-ranks', () => {
      const unrankedPlayers: UnrankedPlayer[] = [
        {...dummyPlayer, rating: 750, id: 1},
        {...dummyPlayer, rating: 1250, id: 2},
        {...dummyPlayer, rating: 1250, id: 3}
      ];

      const rankedPlayersBeforeGame = rankPlayers(unrankedPlayers);
      const playersAfterGame = updateWithGame(rankedPlayersBeforeGame, {
        winner: unrankedPlayers[2],
        loser: unrankedPlayers[1]
      })

      const expected: Player[] = [
        {...unrankedPlayers[2], rating: 1300, rank: 1},
        {...unrankedPlayers[1], rating: 1200, rank: 2},
        {...unrankedPlayers[0], rank: 3}
      ]
      expect(playersAfterGame).toStrictEqual(expected);
    });
  });
});

