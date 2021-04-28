using System;

namespace HashingTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // create 3 users and a hash table
            Users hashTable = new Users();

            User yes = new User();
            User no = new User();
            User same = new User();

            // quickly set all values (name, email, grade)
            no.setAllValues("no", "email@gmail.com", 3);
            same.setAllValues("same", "gmail@email.com", 6);
            yes.setAllValues("yes", "email@email.email", 5);

            // fancy console output
            Console.WriteLine("\n" + "==================================================================" + "\n");

            // generate hash for all 3 users
            no.generateHash(no.getEmail());
            same.generateHash(same.getEmail());
            yes.generateHash(yes.getEmail());

            Console.WriteLine("\n" + "------------------------------------------------------------------" + "\n");

            // store all 3 users into hash table
            hashTable.store(no);
            hashTable.store(same);
            hashTable.store(yes);

            Console.WriteLine("\n" + "------------------------------------------------------------------" + "\n");

            // find user 'same'
            User same2 = new User();
            string email = "gmail@email.com";
            same2 = hashTable.find(email, same2.generateHash(email));

            Console.WriteLine("\n");

            // find user 'yes'
            User yes2 = new User();
            email = "email@email.email";
            yes2 = hashTable.find(email, yes2.generateHash(email));

            Console.WriteLine("\n");

            // find user 'no'
            User no2 = new User();
            email = "email@gmail.com";
            no2 = hashTable.find(email, no2.generateHash(email));

            // fancy console output
            Console.WriteLine("\n" + "==================================================================" + "\n");
        }
    }

    class Users
    {
        // creates the array for hash table
        public User[] hashTable = new User[1000];

        public int store(User user)
        {
            // to store, get hash
            int userHash = user.getHash();
            
            // if there is no user at that hash location, store user there
            if (hashTable[userHash] == null)
                hashTable[userHash] = user;
            else
            {
                while (hashTable[userHash] != null)
                {
                    // can't make multiple accounts with same email
                    if (user.getEmail() == hashTable[userHash].getEmail())
                    {
                        Console.WriteLine($"This email is already in use, perhaps you wanted to search {hashTable[userHash].getName()}?");
                    }

                    // console output and incrementing hash
                    Console.WriteLine("Collision detected! ");
                    userHash++;
                    Console.WriteLine($"Trying index {userHash}");
                }
            }

            // store user and return hash
            Console.WriteLine($"User '{user.getName()}' succesfully stored at index {userHash}.");
            hashTable[userHash] = user;
            return userHash;
        }

        public User find(string email, int hash)
        {
            // if email of user == email of index at hash location
            if (email == hashTable[hash].getEmail())
            {
                Console.WriteLine($"Found user {hashTable[hash].getName()}");
                return hashTable[hash];
            }
            else
            {
                while(hashTable[hash] != null)
                {
                    // console output and increment hash
                    Console.WriteLine("Collision occured, ");
                    hash++;
                    Console.WriteLine($"Trying index {hash}");

                    if (email == hashTable[hash].getEmail())
                    {
                        // if user email == hash index email, break from loop
                        Console.WriteLine($"Found user {hashTable[hash].getName()}");
                        return hashTable[hash];
                    }
                }

                // sad face :(
                Console.WriteLine("Error, user not found :(");
            }

            // this means it didn't work :(
            return null;
        }
    }

    class User 
    {
        // add some items
        private string name;
        private string email;
        private int theoryGrade;
        private int hash;

        // Name setters & getters
        public void setName(string name) { this.name = name; }
        public string getName() { return this.name; }

        // Email setters & getters
        public void setEmail(string email) { this.email = email; }
        public string getEmail() { return this.email; }

        // Theory Grade setters & getters
        public void setTheoryGrade(int grade) { this.theoryGrade = grade; }
        public int getTheoryGrade() { return this.theoryGrade; }

        // Set all values
        public void setAllValues(string name, string email, int grade) {
            this.name = name;
            this.email = email;
            this.theoryGrade = grade;
        }

        // Hash setters & getters
        public void setHash(int hash) { this.hash = hash; }
        public int getHash() { return this.hash; }

        public int generateHash(string email)
        {
            int hash = 1;

            // multiply each ASCII value to eachother
            foreach (byte b in System.Text.Encoding.ASCII.GetBytes(email.ToCharArray()))
                hash *= Convert.ToInt16(b);

            // remainder of hash / 1000
            hash %= 1000;

            // Console output
            Console.WriteLine($"Generated hash {hash} for user {this.name}");

            // user's hash = this hash
            this.hash = hash;

            // return hash
            return hash;
        }
    }
}
