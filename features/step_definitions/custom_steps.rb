When(/^I start the app$/) do
  p "nothing"
end

When(/^canvas scaling is enabled$/) do
  backdoor "EnableCanvasScaling", "http://google.com"
end

When(/^canvas is attached to camera$/) do
  backdoor "EnableCanvasCamera", "http://google.com"
end