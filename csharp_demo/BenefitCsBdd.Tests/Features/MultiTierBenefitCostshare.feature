Feature: MultiTierBenefitCostshare
 A new multi-tier benefit structure that covers
 Medical, dental & Rx major categories. It has 
 separate levels of cost share amount and PCP
 focused provider network
Scenario: Multi_tier medical benefit contains at least two levels of deductible according to number of tiers
 Given The medical benefit has level_one deductible and level_two deductible
 When I inquire the deductible amount
 Then the result should output level_one and level_two deductible
Scenario: Multi_tier medical benefit contains only one max OOP amount regardless number of tiers
 Given The medical benefit has only one max OOP amount
 When I inquire the max OOP amount
 Then the result should output one max OOP amount
Scenario: All tiers claim amount accumulation toward to one OOP amount
 Given The table below contains a sample of insured member medical claims for all tiers
 | MemberId | ProductId | Tier |     ClaimDesc    | Amount |
 | X0001    | ABC00001  | 1    | Office Visit     | 100.00 |
 | X0001    | ABC00001  | 1    | Blood Test       | 50.00  |
 | X0001    | ABC00001  | 2    | X-Ray            | 75.00  |
 | X0001    | ABC00001  | 2    | Specialist Visit | 150.00 |
 | X0002    | ABC00001  | 1    | Office Visit     | 150.00 |
 | X0002    | ABC00001  | 1    | Blood Test       | 125.00 |
 | X0002    | ABC00001  | 2    | X-Ray            | 75.00  |
 | X0002    | ABC00001  | 2    | Specialist Visit | 150.00 |
 | X0002    | ABC00001  | 2    | Tissue Removal   | 220.00 |
 And the max OOP amount is five hundred dollars
 When I inquire a member current OOP amount
 Then the result should be either a sum of claim amounts or its max OOP amount as the table below
 | MemberId | OopAmount |
 | X0001    | 375.00    |
 | X0002    | 500.00    |