require "rubygems"
require "bundler/setup"
require "shellwords"

namespace :build do
  desc "Codegen proper-api files"
  task :proper_api do
    command = "cd ../jigsaw-server && rm -rf tmp/csharp && docker-compose run web rake proper:api:codegen[unity,tmp/csharp]"
    puts command
    `#{command}`
  end

  desc "Build iOS project"
  task :build_ios do
    command = "rm -rf Builds"
    puts command
    `#{command}`
    
    command = "/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod AutoBuilder.PerformiOSBuild"
    puts command
    `#{command}`
  end

  desc "Build Android project"
  task :build_android do
    command = "/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -executeMethod AutoBuilder.PerformAndroidBuild"
    puts command
    `#{command}`
  end

  desc "Cleans the whole repo to initial state"
  task :full_clean do
    command = "git reset --hard && git clean -xdf"
    puts command
    `#{command}`
  end
end