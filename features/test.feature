Feature: Test feature
  Background:
    When I start the app

  Scenario: Test scenario
  	And I should not see "HUITA EBALA"
    And I should see a "OK" button
    And I touch "OK"
    Then I should see "HUITA EBALA"