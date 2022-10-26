import sys

# Winner flags
P1 = 1
P2 = 2

def combat(p1_deck, p2_deck):

    while len(p1_deck) > 0 and len(p2_deck) > 0:
        p1 = p1_deck.pop(0)
        p2 = p2_deck.pop(0)

        if p1 > p2:
            p1_deck.append(p1)
            p1_deck.append(p2)
        else:
            p2_deck.append(p2)
            p2_deck.append(p1)

    if len(p1_deck) == 0:
        return (P2, p2_deck)
    else:
        return (P1, p1_deck)

def recursive_combat(p1_deck, p2_deck):

    previous_rounds = []

    while len(p1_deck) > 0 and len(p2_deck) > 0:

        # Before either player deals a card, if there was a previous round in this game 
        # that had exactly the same cards in the same order in the same players' decks
        # the game instantly ends in a win for player 1.
        round_cards = (tuple(p1_deck), tuple(p2_deck))
        if round_cards in previous_rounds:
            return (P1, p1_deck)
            
        previous_rounds.append(round_cards)

        p1 = p1_deck.pop(0)
        p2 = p2_deck.pop(0)

        if len(p1_deck) >= p1 and len(p2_deck) >= p2:
            # winner is decided by recursively playing combat
            winner = recursive_combat(p1_deck[:p1], p2_deck[:p2])[0]
        else:
            # at least one player must not have enough cards left in their deck to recurse; the winner of the round is the player with the higher-value card.
            winner = P1 if p1 > p2 else P2 

        if winner == P1:
            p1_deck.append(p1)
            p1_deck.append(p2)
        else:
            p2_deck.append(p2)
            p2_deck.append(p1)

    if len(p1_deck) == 0:
        return (P2, p2_deck)
    else:
        return (P1, p1_deck)

def get_score(winner):
    winning_deck = winner[1]
    return sum([value * (len(winning_deck) - index) for (index, value) in enumerate(winning_deck)])

def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        input = f.read().splitlines()

    hand_size = int((len(input) - 3) / 2)
    p1_deck = [int(input[i]) for i in range(1, hand_size + 1)]
    p2_deck = [int(input[i]) for i in range(hand_size + 3, len(input))]

    combat_winner = combat(p1_deck[:], p2_deck[:])
    score = get_score(combat_winner)
    print(f"Part One: P{combat_winner[0]} wins with score {score}")

    recursive_winner= recursive_combat(p1_deck[:], p2_deck[:])
    score = get_score(recursive_winner)
    print(f"Part Two: P{recursive_winner[0]} wins with score {score}")

if __name__ == "__main__":
    main()