import sys
import re

valid_ecl = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"]

def is_valid(passport):

    if not has_required_fields(passport):
        return False

    # byr (Birth Year) - four digits; at least 1920 and at most 2002.
    if not (1920 <= int(passport["byr"]) <= 2002):
        return False

    # iyr (Issue Year) - four digits; at least 2010 and at most 2020.
    if not (2010 <= int(passport["iyr"]) <= 2020):
        return False

    # eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
    if not (2020 <= int(passport["eyr"]) <= 2030):
        return False

    # hgt (Height) - a number followed by either cm or in:
    hgt_match = re.match("(\d+)(cm|in)", passport["hgt"])
    if not hgt_match:
        return False
    hgt_value = int(hgt_match.group(1))
    hgt_unit = hgt_match.group(2)
    # If cm, the number must be at least 150 and at most 193.
    if hgt_unit == "cm" and not (150 <= hgt_value <= 193):
        return False
    # If in, the number must be at least 59 and at most 76.
    if hgt_unit == "in" and not (59 <= hgt_value <= 76):
        return False

    # hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
    if not re.match("#[a-f0-9]{6}", passport["hcl"]):
        return False

    # ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
    if passport["ecl"] not in valid_ecl:
        return False

    # pid (Passport ID) - a nine-digit number, including leading zeroes.
    if not re.match("^\d{9}$", passport["pid"]):
        return False

    # cid (Country ID) - ignored, missing or not.

    #print(f'Accepted: {passport}')
    return True

def has_required_fields(passport):
    return len(passport) == 8 or (len(passport) == 7 and not 'cid' in passport.keys())

def read_passports(lines):
    passports = []
    current_passport = {}
    for line in lines:
        if line:
            key_value_pairs = line.split(" ")
            passport_entries = list(map(lambda kvp: tuple(kvp.split(":")), key_value_pairs))
            current_passport.update(passport_entries)
        else:
            passports.append(current_passport)
            current_passport = {}
    passports.append(current_passport)
    return passports

with open(sys.argv[1]) as f:
    lines = f.read().splitlines() 

passports = read_passports(lines)

passports_with_required_fields = sum(map(has_required_fields, passports))
print(f'Part One: {passports_with_required_fields}')

valid_passports = sum(map(is_valid, passports))
print(f'Part Two: {valid_passports}') 