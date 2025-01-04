using Contact.Data;
using Contact.Model;
using Contact.Model.Contact;
using Contact.Model.Phone;

namespace Contact.Business
{
    public class ContactBusiness
    {
        private ContactData contactData;

        public ContactBusiness() 
        {
            this.contactData = new ContactData();
        }

        // ...
        public BusinessResult<IEnumerable<PhoneTypeTable>> GetPhoneTypesBusiness()
        {
            BusinessResult<IEnumerable<PhoneTypeTable>> result = new BusinessResult<IEnumerable<PhoneTypeTable>>();

            IEnumerable<PhoneTypeTable> phoneTypes = contactData.GetPhoneTypes();


            if (!phoneTypes.Any())
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(phoneTypes);
            }

            return result;
        }

        // ...
        public BusinessResult<bool> AddContactBusiness(ContactTable contact)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();

            string guid = Guid.NewGuid().ToString().Replace("-", "");

            string avatar = contact.Avatar.Replace("data:image/png;base64,", "");

            byte[] avatarData = Convert.FromBase64String(avatar);

            string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Contact\Avatar\")}{guid}.png";


            if (File.Exists(file))
            {
                File.Delete(file);
            }

            File.WriteAllBytes(file, avatarData);

            contact.Avatar = guid.ToString();


            try
            {
                contactData.AddContact(contact);

                result.SetData(true);
            }
            catch (Exception) 
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> AddPhoneBusiness(PhoneTable phone, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.AddPhone(phone, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> AddFavoriteBusiness(FavoriteTable favorite, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.AddFavorite(favorite, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> EditContactBusiness(ContactTable contact)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.EditContact(contact);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> EditPhoneBusiness(PhoneTable phone, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.EditPhone(phone, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<IEnumerable<PhoneTable>> GetPhonesBusiness(int contactId, int userId)
        {
            BusinessResult<IEnumerable<PhoneTable>> result = new BusinessResult<IEnumerable<PhoneTable>>();

            IEnumerable<PhoneTable> phones = contactData.GetPhones(contactId, userId);


            if (!phones.Any())
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(phones);
            }

            return result;
        }

        // ...
        public BusinessResult<ContactTable> GetContactBusiness(int contactId, int userId)
        {
            BusinessResult<ContactTable> result = new BusinessResult<ContactTable>();

            ContactTable contact = contactData.GetContact(contactId, userId);


            if (contact.Id == 0)
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                string file = @$"{Path.Combine(Directory.GetCurrentDirectory(), @"Static\Contact\Avatar\")}{contact.Avatar}.png";

                contact.Avatar = "data:image/png;base64,";

                contact.Avatar += Convert.ToBase64String(File.ReadAllBytes(file));

                result.SetData(contact);
            }

            return result;
        }

        // ...
        public BusinessResult<IEnumerable<ContactTable>> GetContactsBusiness(int userId)
        {
            BusinessResult<IEnumerable<ContactTable>> result = new BusinessResult<IEnumerable<ContactTable>>();

            IEnumerable<ContactTable> contacts = contactData.GetContacts(userId);


            if (!contacts.Any())
            {
                result.SetError(500, "Something went wrong.");
            }
            else
            {
                result.SetData(contacts);
            }

            return result;
        }

        // ...
        public BusinessResult<bool> RemoveContactBusiness(int contactId, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.RemoveContact(contactId, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> RemovePhoneBusiness(int phoneId, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.RemovePhone(phoneId, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
        public BusinessResult<bool> RemoveFavoriteBusiness(int contactId, int userId)
        {
            BusinessResult<bool> result = new BusinessResult<bool>();


            try
            {
                contactData.RemoveFavorite(contactId, userId);

                result.SetData(true);
            }
            catch (Exception)
            {
                result.SetError(500, "Something went wrong.");
            }

            return result;
        }

        // ...
    }
}
