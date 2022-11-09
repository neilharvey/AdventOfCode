import sys

def transform_value(value, subject_number):
    value = value * subject_number
    value = value % 20201227   
    return value

def transform(subject_number, loop_size):
    value = 1
    for _ in range(1, loop_size + 1):
        value = transform_value(value, subject_number)

    return value      

def main():

    path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
    with open(path) as f:
        card_public_key = int(f.readline())
        door_public_key = int(f.readline())

    map = {0: 1}
    loop_size = 1
    value = 1

    # we only need one of the two loop sizes because both the door and the key
    # can produce the encryption key
    while not card_public_key in map.keys() and not door_public_key in map.keys():    
        value = transform_value(value, subject_number=7)
        map[value] = loop_size
        loop_size += 1

    if card_public_key in map.keys():
        card_loop_size = map[card_public_key]
        print(f"Card loop size: {card_loop_size}")
        encryption_key = transform(door_public_key, card_loop_size)
    else:        
        door_loop_size = map[door_public_key]
        print(f"Door loop size: {door_loop_size}")
        encryption_key = transform(card_public_key, door_loop_size)

    print(f"Encryption key: {encryption_key}")

if __name__ == "__main__":
    main()