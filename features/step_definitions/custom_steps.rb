When(/^I start the app$/) do
  backdoor "setBaseURL:", "http://google.com"
end