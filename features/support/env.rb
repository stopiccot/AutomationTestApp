require 'calabash-cucumber'
require 'calabash-cucumber/launcher'
require 'calabash-cucumber/cucumber'
require 'calabash-cucumber/calabash_steps'

calabash_launcher = Calabash::Cucumber::Launcher.new

options = {
  app: "Buils/iOS/AutomationTestApp.ipa",
  uia_strategy: :preferences,
  launch_method: :instruments,
  device_target: "iPhone 6 (9.3)",
  device: "iPhone 6 (9.3)"
}

calabash_launcher.relaunch(options)