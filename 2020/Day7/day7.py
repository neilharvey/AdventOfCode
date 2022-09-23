import sys
import re

def get_rules(input):
  rules = {}

  for line in input:
    line_match = re.search("([a-z\s]*)\sbags contain\s(.*)", line)
    bag_colour = line_match.group(1)
    contents_list = line_match.group(2).split(", ")
    contents = {}
    for item in contents_list:
      contents_match = re.search("(\d)\s([a-z\s]*)\sbag", item)
      if contents_match:
        contents[contents_match.groups()[1]] = int(contents_match.groups()[0])
    
    rules[bag_colour] = contents

  return rules

def get_possible_containers(rules, bag_color):
  direct_containers = set()
  for rule in rules:
    if bag_color in rules[rule].keys():
      direct_containers.add(rule)

  all_containers = set(direct_containers)
  for bag in direct_containers:
    bag_containers = get_possible_containers(rules, bag)
    all_containers = all_containers | bag_containers

  return all_containers
    
def get_total_bags(rules, bag_colour):
  total_bags = 0
  contents = rules[bag_colour]
  for bag in contents:
    total_bags += contents[bag] + (contents[bag] * get_total_bags(rules, bag))

  return total_bags

def highlight(value):
  return f"\033[1m{value}\033[0m"

with open(sys.argv[1]) as f:
   input = f.read().splitlines()

rules = get_rules(input)
possible_containers = len(get_possible_containers(rules, "shiny gold"))
total_bags = get_total_bags(rules, "shiny gold")
print(f"Part One: {highlight(possible_containers)} bags can contain at least one shiny gold bag")
print(f"Part Two: A shiny gold must contain {highlight(total_bags)} other bags")
