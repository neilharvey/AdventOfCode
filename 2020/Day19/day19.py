import sys
import re

# depth here was determined through manually testing different values
def create_regex(rule_number='0', depth=15):
    if depth == 0:
        return ""

    rule = rules[rule_number]
    if '\"' in rule:
        return rule.replace('\"',"")

    regex = ""    
    for sub_rule in rule.split(" "):
        if sub_rule.isnumeric():
            regex += create_regex(sub_rule, depth-1)
        else: 
            regex += sub_rule # "|"

    if "|" in rule:
        return "(" + regex + ")"
    else:
        return regex

path = sys.argv[1] if len(sys.argv) > 1 else "example.txt"
with open(path) as f:
    input = f.read().splitlines()

rules = {}
words = []
for line in input:
    if ":" in line:
        (rule_number, rule_text) = line.split(": ")
        rules[rule_number] = rule_text
    elif len(line) > 0:
        words.append(line)

pattern = re.compile(create_regex())
matching_words = [word for word in words if pattern.fullmatch(word)]
print(f"Part One: {len(matching_words)}")

rules['8'] = '42 | 42 8'
rules['11'] = '42 31 | 42 11 31'
pattern = re.compile(create_regex())
matching_words = [word for word in words if pattern.fullmatch(word)]
print(f"Part Two: {len(matching_words)}")