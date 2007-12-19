class Officer

end

describe "All Employees", :shared => true do
  it "should be payable" do
    @employee.should respond_to(:calculate_pay)
  end
end

describe "All Managers", :shared => true do
  it_should_behave_like "All Employees"
  it "should be bonusable" do
    @employee.should respond_to(:apply_bonus)
  end
end

describe Officer do
  before(:each) do
    @employee = Officer.new
  end
  it_should_behave_like "All Managers"

  it "should be optionable" do
    @employee.should respond_to(:grant_options)
  end
end

describe "another example group" do
    it "should shit" do
        puts "shit happened"
    end
end