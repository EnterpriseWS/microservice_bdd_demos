Feature: FlexAdvantageBenefit
	A new multi-tier benefit structure that covers
	Medical, dental & Rx major categories. It has 
	separate levels of co-insurance amount and PCP
	focused provider network

Scenario: Multi_tier medical benefit contains at least two levels of deductible
	Given The medical benefit has level one deductible and level two deductible
	When I inquire the deductible amount
	Then the result should output level one and level two deductible 

Scenario: Multi-tier medical benefit contains only one max OOP amount
	Given The medical benefit has only one max OOP amount
	When I inquire the max OOP amount
	Then the result should output one max OOP amount 

Scenario: All tiers claim amount accumulation toward to OOP amount
	Given The table below records insured member medical claim for all tiers
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
	When I inquire a member current OOP amount
	Then the result should output a summation of all claims
