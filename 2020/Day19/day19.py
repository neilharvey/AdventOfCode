import sys
import re

def create_regex(rules, rule_number):
    rule = rules[rule_number]
    if '\"' in rule:
        return rule.replace('\"',"")

    regex = ""    
    for sub_rule in rule.split(" "):
        if sub_rule.isnumeric():
            regex += create_regex(rules, sub_rule)
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

rule_regex = create_regex(rules, '0')
pattern = re.compile(f"^{rule_regex}$")
matching_words = [word for word in words if pattern.match(word)]
print(f"Part One: {len(matching_words)}")