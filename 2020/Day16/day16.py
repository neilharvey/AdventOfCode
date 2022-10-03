import sys
import re
import math

ticket_field_pattern = re.compile("([a-z ]*): (\d+)-(\d+) or (\d+)-(\d+)")
path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"

# Read the data
with open(path) as f:
    
    # read fields
    fields = {}
    line = f.readline()
    while len(line) > 1:
        match = re.search(ticket_field_pattern, line)
        fields[match.group(1)] = (int(match.group(2)), int(match.group(3)), int(match.group(4)), int(match.group(5)))
        line = f.readline()

    # your ticket
    f.readline() 
    your_ticket = list(map(int,f.readline().strip().split(",")))
    f.readline()

    # nearby tickets
    f.readline()
    nearby_tickets = []
    line = f.readline()
    while len(line) > 1:
        nearby_tickets.append(list(map(int,line.strip().split(","))))
        line = f.readline()

# Work out which tickets are invalid
invalid_values = []
for ticket in nearby_tickets:
    for value in ticket:
        is_valid = False
        for name in fields:
            lower_range = fields[name][0:2]
            upper_range = fields[name][2:4]
            if lower_range[0] <= value <= lower_range[1] or upper_range[0] <= value <= upper_range[1]:
                is_valid = True
        if not is_valid:
            invalid_values.append(value)
    
print(f"Part One: {sum(invalid_values)}")

# Discard invalid tickets
valid_tickets = [t for t in nearby_tickets if len(set(t).intersection(invalid_values)) == 0]

# Determine which fields could be valid for each column
possible_fields = {}
for col in range(0, len(fields)):
    possible_fields[col] = set()
    for name in fields:
        lower_range = fields[name][0:2]
        upper_range = fields[name][2:4]    
        is_valid = True
        for ticket in valid_tickets:
            value = ticket[col]
            if not(lower_range[0] <= value <= lower_range[1] or upper_range[0] <= value <= upper_range[1]):
                is_valid = False
                break
        if is_valid:
            possible_fields[col].add(name)

# Use the columns we know about to determine the remaining possibilities
while max(map(lambda x: len(possible_fields[x]), possible_fields)) > 1:
    for col in possible_fields:
        if len(possible_fields[col]) == 1:
            field = list(possible_fields[col]).pop()
            for other_col in possible_fields: 
                if field in possible_fields[other_col] and other_col != col:
                    possible_fields[other_col].remove(field)

ticket_values = {list(field)[0]:your_ticket[index] for (index,field) in possible_fields.items()}
ticket_product = math.prod([value for (field, value) in ticket_values.items() if field.startswith('departure')])
print(f"Part Two: {ticket_product}")