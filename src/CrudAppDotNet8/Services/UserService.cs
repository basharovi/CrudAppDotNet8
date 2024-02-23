using AutoMapper;
using BCrypt.Net;
using CrudAppDotNet8.Entities;
using CrudAppDotNet8.Helpers;
using CrudAppDotNet8.Models.Users;

namespace CrudAppDotNet8.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Create(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);
}

public class UserService
    (
        DataContext context,
        IMapper mapper
    ) : IUserService
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return GetUser(id);
    }

    public void Create(CreateRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        //user.PasswordHash = BCrypt.HashPassword(model.Password);
        user.PasswordHash = (model.Password);

        // save user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = GetUser(id);

        // validate
        if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            //user.PasswordHash = BCrypt.HashPassword(model.Password);
            user.PasswordHash = (model.Password);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = GetUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // helper private methods

    private User GetUser(int id)
    {
        var user = _context.Users.Find(id);

        return user ?? throw new KeyNotFoundException("User not found");
    }
}
