require 'calabash-cucumber'
require 'calabash-cucumber/launcher'
require 'calabash-cucumber/cucumber'
require 'calabash-cucumber/calabash_steps'

calabash_launcher = Calabash::Cucumber::Launcher.new

app_path = "./Builds/iOS/build/Release-iphonesimulator/AutomationTestApp.app"

options = {
  app: app_path,
  # uia_strategy: :preferences,
  # launch_method: :instruments,
  device_target: "iPhone 6 (9.3)",
  device: "iPhone 6 (9.3)"
}

# xcrun simctl launch booted com.one1eleven.AutomationTestApp
# xcrun instruments -w 306845D4-622F-4ABE-A63D-B9E907CD124D -t Automation com.one1eleven.AutomationTestApp
# ENV["APP"] = app_path
ENV["APP_BUNDLE_PATH"] = app_path

calabash_launcher.relaunch(options)