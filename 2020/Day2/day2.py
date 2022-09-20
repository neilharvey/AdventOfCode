import sys
import re

pattern = re.compile("(\d{1,2})-(\d{1,2}) ([a-z]): ([a-z]*)")

def is_valid_sled_password(l, u, letter, password):
    occurrences = password.count(letter)
    return occurrences >= l and occurrences <= u

def is_valid_toboggan_password(l, u, letter, password):
    return (password[l - 1] == letter) ^ (password[u - 1] == letter)

def parse_password_policy(input):
    match = re.search(pattern, input)
    l = int(match.group(1))
    u = int(match.group(2))
    letter = match.group(3)
    password = match.group(4)
    return (l, u, letter, password)

path = sys.argv[1]
with open(path) as f:
   lines = f.readlines()

passwords = list(map(parse_password_policy, lines))

valid_sled_passwords = sum(map(lambda t: is_valid_sled_password(*t), passwords))
print(f'Part One: {valid_sled_passwords}')

valid_toboggan_passwords = sum(map(lambda t: is_valid_toboggan_password(*t), passwords))
print(f'Part Two: {valid_toboggan_passwords}')